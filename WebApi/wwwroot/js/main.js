requirejs.undef();

requirejs.config({
    baseUrl: 'js',
    paths: {
        text: "../node_modules/text/text",
        jquery: "../node_modules/jquery/dist/jquery",
        knockout: "../node_modules/knockout/build/output/knockout-latest.debug",
        bootstrap: "../node_modules/bootstrap/dist/js/bootstrap.bundle",
        titleService: "services/titleService",
        userService: "services/userService",
        myEventListener: "services/myEventListener",
    }
});

requirejs(['jquery', 'bootstrap'], function ($, _) {
    //loaded and can be used here now.
});

// component registration
require(['knockout'], (ko) => {
    ko.components.register("pagination", {
        viewModel: { require: "components/pagination/pagination" },
        template: { require: "text!components/pagination/pagination.html" }
    });
    ko.components.register("loading", {
        viewModel: { require: "components/loading/loading" },
        template: { require: "text!components/loading/loading.html" }
    });
    ko.components.register("home", {
        viewModel: { require: "components/home/home" },
        template: { require: "text!components/home/home.html" }
    });
    ko.components.register("search", {
        viewModel: { require: "components/search/search" },
        template: { require: "text!components/search/search.html" }
    });
    ko.components.register("account-connected", {
        viewModel: { require: "components/account/connected/connected" },
        template: { require: "text!components/account/connected/connected.html" }
    });
    ko.components.register("account-disconnected", {
        viewModel: { require: "components/account/disconnected/disconnected" },
        template: { require: "text!components/account/disconnected/disconnected.html" }
    });
});

requirejs(["knockout", "viewmodel"], function (ko, vm) {
    ko.applyBindings(vm);
});
