import {promises as fs} from 'fs';
import esbuild from 'esbuild';

/**
 * Builds the app, using the Main.fs as entry point.
 * @param sourceDir {string} Directory containing the entryFile
 * @param entryFile {string} Js File containing the project entry
 * @param destinationDir {string} Directory to write the built files to
 * @param productionMode {boolean} If set to true; Uses minification, doesn't produce a sourcemap, if false watches input and automatically refreshes
 * @returns {Promise<void>} Will resolve once it's built successfully
 */
const bundle = async (sourceDir, entryFile, destinationDir, productionMode = false) => {
    const runBuild = options => {
        if (productionMode)
            return esbuild.build(options);
        else
            return esbuild.serve({
                servedir: destinationDir,
                port: 3000,
            }, options);
    }

    let func = productionMode ? esbuild.build : esbuild.context;
    const initialResult = await func(
        {
            entryPoints: {
                app: `${sourceDir}/${entryFile}`,
            },
            bundle: true,
            // Required for Redux DevTools / RemoteDev
            define: { 'global': 'window' },
            // Only minify in production, leads to faster build time on dev
            minify: productionMode,
            // Only generate sourcemaps in dev
            sourcemap: !productionMode,
            // Folder to put all generated files
            outdir: destinationDir,
        });

    if (!productionMode) {
        const serveRes = await initialResult.serve({
            servedir: destinationDir,
            port: 3000,
        });
        console.log(`devserver running on http://${serveRes.host}:${serveRes.port}`);
    }
}

/**
 * Copies a folder recursively
 * @param sourceDir Directory to be copied
 * @param destinationDir Directory to copy to
 * @returns {Promise<void>} Will resolve once everything is done
 */
const copyAssets = async (sourceDir, destinationDir) => {
    await fs.mkdir(destinationDir, {recursive: true});
    const files = await fs.readdir(sourceDir);
    files.map(async file => {
        const source = `${sourceDir}/${file}`;
        const destination = `${destinationDir}/${file}`;
        const stats = await fs.stat(source);
        if (stats.isDirectory()) {
            await copyAssets(source, destination);
        } else {
            await fs.copyFile(source, destination);
        }
    });
}

const processArgs = () => {
    const cliArgs = process.argv.slice(2);

    const entryFile = cliArgs.find(a => a.startsWith("-e="))?.slice(3); // entry-file override
    const productionMode = cliArgs.includes('-p'); // production flag
    const outDir = cliArgs.find(a => a.startsWith('-o='))?.slice(3); // output-dir override

    return {
        productionMode,
        entryFile,
        outDir,
    }
}

const {productionMode, entryFile, outDir} = processArgs();

const jsEntryDir = '.build';
const jsEntryFile = entryFile ?? 'App.js'
const destinationDir = outDir ?? './out';
console.log(`building in ${productionMode ? 'production' : 'development'} mode to '${destinationDir}'`);

await copyAssets('public', destinationDir);
await bundle(jsEntryDir, jsEntryFile, destinationDir, productionMode);
