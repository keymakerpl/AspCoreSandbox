/** klasa ts. Export oznacza że jest to moduł, nie jest z tym globalny */
var StoreCustomer = /** @class */ (function () {
    /** konstruktor w js, parametry z konstrukotra automatycznie tworzone są w klasie - nie trzeba ich deklarować itd. */
    function StoreCustomer(firstName, lastname) {
        this.firstName = firstName;
        this.lastname = lastname;
        this.visit = 0;
    }
    /** funkcja */
    StoreCustomer.prototype.showName = function () {
        alert(this.firstName + " " + this.lastname);
    };
    Object.defineProperty(StoreCustomer.prototype, "name", {
        get: function () { return this.ourName; },
        /** jak wygląda "włąściwość" w ts - getter i setter, this jest wymagane w ts */
        set: function (val) { this.ourName = val; },
        enumerable: true,
        configurable: true
    });
    return StoreCustomer;
}());
//# sourceMappingURL=storecustomer.js.map