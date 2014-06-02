/// <reference path="scripts/rqtst.js' />
/// <reference path="scripts/sinon-1.8.2.js" />
/// <reference path="scripts/sinon-qunit-1.0.0.js" />

/// <reference path="../mvckatana2/scripts/knockout-3.0.0.debug.js" />
/// <reference path="../mvckatana2/scripts/knockout-projections.js" />
/// <reference path="../mvckatana2/scripts/knockout.mapping-latest.debug.js" />

/// <reference path="../mvckatana2/scripts/ticketList.js" />

module('ticket tests', {
    setup: function() {
        //use real Knockout, but mock the XHR dependency
        xhr = {jsonPost : sinon.stub() };
        TicketList = initModule(ko, xhr, ko.mapping);
    },
});

test('creates VM and sets default values', function () {
    //WHEN sut is created
    var sut = new TicketList();

    //THEN sut should have basic properties defined
    equal(sut.backlog().length, 0);
    equal(sut.working().length, 0);
    equal(sut.done().length, 0);
});

test('can load data', function () {
    var sut = new TicketList();

    //GIVEN 3 tickets on the server side, one in each state
    xhr.jsonPost.returns(promise.done([{ id: 1, state: 'backlog' }, { id: 2, state: 'working' }, { id: 3, state: 'done' }]));

    //WHEN sut is asked to load the data
    sut.loadData();

    //THEN sut should put the tickets in each respective bucket
    equal(sut.backlog().length, 1);
    equal(sut.backlog()[0].id(), 1);

    equal(sut.working().length, 1);
    equal(sut.working()[0].id(), 2);

    equal(sut.done().length, 1);
    equal(sut.done()[0].id(), 3);
});

test('change state', function() {
    var sut = new TicketList();

    //GIVEN a ticket
    var b = { id: 123, state : ko.observable('working') };

    //WHEN the ticket is moved to 'done'
    sut.changeState(b, 'done');

    //THEN the ticket should be passed as the second parameter to jsonPost
    equal(xhr.jsonPost.args[0][1].id, 123);
    equal(xhr.jsonPost.args[0][1].state, 'done');
});

test('update', function () {
    var sut = new TicketList();

    //GIVEN a ticket in a working state
    sut.tickets(ko.mapping.fromJS([{ id: 1, state: 'backlog' }, { id: 2, state: 'working' }, { id: 3, state: 'done' }])());

    equal(sut.backlog().length, 1);
    equal(sut.working().length, 1);
    equal(sut.done().length, 1);

    //WHEN move ticket is called, with new state = 'done'
    sut.updateTicket({ id: 2, state: 'done' });

    //THEN it should be removed from the 'working' array
    equal(sut.working().length, 0);

    //AND added to the 'done' array
    equal(sut.done().length, 2);
});

test('apply bindings', function () {
    var sut = new TicketList();
    sinon.stub(ko, 'applyBindings');
    
    //WHEN applyBindings is called
    sut.applyBindings('#somediv');

    //THEN ko's applyBindings is called with sut and id
    ok(ko.applyBindings.calledWith(sut, '#somediv'));
});
