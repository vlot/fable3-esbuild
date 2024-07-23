// yarn needs a dependency called "remotedev" to resolve the unused remotedev
// imports in Fable.Elmish.Debugger code that is never actually used in our case.
// This minimal file mirrors the external API of remotedev.
export const connect = () => {};
export const extractState = () => {};
