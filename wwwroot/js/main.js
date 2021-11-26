/// <reference path="lib/jquery/dist/jquery.min.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />


requirejs.config({
    baseUrl: 'js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery",
        knockout: "../node_modules/knockout/build/output/knockout-latest.debug",
        bootstrap: "../node_modules/bootstrap/dist/js/bootstrap.bundle",
        dataService: "services/dataService",
        titleService: "services/titleService",
    }
});

requirejs(['jquery', 'bootstrap'], function ($, boot) {
    //loaded and can be used here now.
});

requirejs(["knockout", "viewmodel"], function (ko, vm) {
    ko.applyBindings(vm);
});
