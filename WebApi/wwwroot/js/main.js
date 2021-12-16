requirejs.undef();

requirejs.config({
    baseUrl: 'js',
    paths: {
        text: "../node_modules/text/text",
        jquery: "../node_modules/jquery/dist/jquery",
        redux: "../node_modules/redux/dist/redux",
        knockout: "../node_modules/knockout/build/output/knockout-latest.debug",
        bootstrap: "../node_modules/bootstrap/dist/js/bootstrap.bundle",
        titleService: "services/titleService",
        userService: "services/userService",
        storeService: "services/storeService",
        simpleSearchService: "services/simpleSearchService",
        siteFunctionsService: "services/siteFunctionsService",
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
    ko.components.register("profile", {
        viewModel: { require: "components/account/info/info" },
        template: { require: "text!components/account/info/info.html" }
    });
    ko.components.register("bookmark-titles", {
        viewModel: { require: "components/bookmark/title/title" },
        template: { require: "text!components/bookmark/title/title.html" }
    });
    ko.components.register("bookmark-names", {
        viewModel: { require: "components/bookmark/name/name" },
        template: { require: "text!components/bookmark/name/name.html" }
    });
    ko.components.register("titles", {
        viewModel: { require: "components/titles/titles" },
        template: { require: "text!components/titles/titles.html" }
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
