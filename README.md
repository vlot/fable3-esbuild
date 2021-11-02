# Fable3 Sample Project using esbuild

This is a simple Fable app which implements counter functionality using [Elmish](https://elmish.github.io).
It can be easily extended to suit your needs.

## Features
- Full Fable.React F# to bundled JS pipeline
- Elmish support with Redux Devtools out-of-the-box
- Lightning-fast builds using esbuild
- Integrated dev server with auto-rebuilding

## Prerequisites
- [dotnet SDK](https://dotnet.microsoft.com/download) 3.1 or higher
- [NodeJS](https://nodejs.org/en/) v14.8 or newer
- Optional: [Yarn Package Manager](https://yarnpkg.com/)

## Getting started
- Clone this repository to your local disk
- Run `yarn install` to pull all dependencies (This will also invoke `dotnet tool restore` to install dotnet dependencies)
- Run `yarn start` to build the project and start the dev server which will be available at `http://localhost:3000` 

Whenever you change an F# source file, it will update the dev server. You still need to manually reload the browser though.

## Project structure

### Yarn (or npm)
- JS dependencies are declared in `package.json` and `yarn.lock` is an automatically generated lock file keeping track of the actually installed versions

### EsBuild
EsBuild is a fast, lightweight JS bundler which can be easily extended to suit your JS bundle needs.
Fable doesn't directly interact with it but rather they share the `.build` folder in the project root.
The build or dev server configuration is located in `build.mjs`. It includes a separate workflow for development and production builds since the esbuild API separates them that way.
You can find more information to esbuild [here]()

### F#
This sample project contains one F# source file `App.fs` and the project file `App.fsproj` in the `src` folder

### Web assets
The `index.html` file is the only asset contained in this sample project. You can add more in the `public` folder. They will be copied to the destination folders automatically but they will only be moved at the start and not automatically after each change.

## Caveats
Some things don't fully work yet in the esbuild setup:
1. Source Maps aren't fully supported as they don't work through the builtin dev server. They do work in full builds.
    In most cases this isn't an issue because the Elmish architecture allows for nice debugging in the editor.
2. esbuild doesn't support hot module replacement or hot reloading.
3. This example uses the file system to cache the built JS-files.
    This might cause errors where moved or deleted files are not properly updated.
    You can resolve these issues by manually deleting the `.build` folder.