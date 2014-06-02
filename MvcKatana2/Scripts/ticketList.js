define(['knockout', 'xhr', 'knockout.mapping', 'knockout.projections'], function (ko, xhr, mapping) {
    return function () {
        var self = this;
        
        self.tickets = ko.observableArray();

        self.backlog = self.tickets.filter(function (x) { return x.state() === 'backlog'; });
        self.working = self.tickets.filter(function (x) { return x.state() === 'working'; });
        self.done = self.tickets.filter(function (x) { return x.state() === 'done'; });

        self.changeState = function (ticket, newState) {
            ticket.state(newState);
            xhr.jsonPost('/api/tickets/changeState', mapping.toJS(ticket));
        };

        self.updateTicket = function (ticket) {
            var match = ko.utils.arrayFirst(self.tickets(), function (item) {
                return ticket.id === item.id();
            });

            match.state(ticket.state);
        };

        self.loadData = function () {
            xhr.jsonPost('/api/tickets/getAll').done(function (data) {
                self.tickets(mapping.fromJS(data)());
            });
        };

        self.applyBindings = function (id) {
            ko.applyBindings(self, id);
        };
    };
});
