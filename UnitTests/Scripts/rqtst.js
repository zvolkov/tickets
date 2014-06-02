function define(deps, moduleInitializer) {
    //(0, eval)('this')
    window.initModule = moduleInitializer;
};

window.promise = {
    done: function (result) {
        return { done: function (onDone) { onDone(result); } };
    }
};