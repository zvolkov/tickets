var require = {
    baseUrl: '/Scripts',
    paths: {
        jquery: [
             '//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min',
             'jquery-1.11.0.min'
        ],
        knockout: [
            '//ajax.aspnetcdn.com/ajax/knockout/knockout-3.0.0',
            'knockout-3.0.0'
        ],
        'jquery.signalR': 'jquery.signalR-2.0.2.min',
        'signalR.proxy': '/signalr/hubs?',
        ravenjs: '//cdn.ravenjs.com/1.1.10/jquery,native/raven.min',
        'jquery.bootstrap': 'bootstrap.min',
        'knockout.projections': 'knockout-projections',
        'knockout.mapping': 'knockout.mapping-latest'
    },
    shim: {
            'jquery.signalR': { deps: ['jquery'] },
            'signalR.proxy': {
                deps: ['jquery.signalR'],
                exports: '$.connection'
            },
            ravenjs: {
                deps: ['jquery'],
                exports: 'Raven',
                init: function() {
                    this.Raven.config('https://df069977eb1c400eb3c98711475aa566@app.getsentry.com/19527').install();
                }
            },
            'jquery.bootstrap': { deps: ['jquery'] }
    },
    deps: ['ravenjs' /*, 'jquery.bootstrap'*/] //auto-load on page startup
};