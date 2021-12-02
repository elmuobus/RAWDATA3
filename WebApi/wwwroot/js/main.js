requirejs.undef();

requirejs.config({
    baseUrl: 'js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery",
        knockout: "../node_modules/knockout/build/output/knockout-latest.debug",
        bootstrap: "../node_modules/bootstrap/dist/js/bootstrap.bundle",
        titleService: "services/titleService",
        userService: "services/userService",
        accountModel: "model/accountModel",
        headerModel: "model/headerModel",
        homeModel: "model/homeModel",
        paginationModel: "model/paginationModel",
    }
});

requirejs(['jquery', 'bootstrap'], function ($, _) {
    //loaded and can be used here now.
    
    $(() => {
        const includes = $('[data-include]')
        $.each(includes, function() {
            const file = 'html/' + $(this).data('include') + '.html';
            $(this).load(file)
        })
    })
});

requirejs(["knockout", "viewmodel"], function (ko, vm) {
    ko.applyBindings(vm);
});
