// custom-header.js

(() => {
    const originalFetch = window.fetch;
    window.fetch = function (input, init = {}) {
        init.headers = init.headers || {};
        init.headers["Cliente"] = "Resiliencia";
        return originalFetch(input, init);
    };
})();
