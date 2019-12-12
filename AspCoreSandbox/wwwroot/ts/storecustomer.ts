/** klasa ts. Export oznacza że jest to moduł, nie jest z tym globalny */

class StoreCustomer {

    public visit: number = 0;
    private ourName: string;

    /** konstruktor w js, parametry z konstrukotra automatycznie tworzone są w klasie - nie trzeba ich deklarować itd. */
    constructor(private firstName: string, private lastname: string) { }

    /** funkcja */
    public showName() {
        alert(this.firstName + " " + this.lastname);
    }

    /** jak wygląda "włąściwość" w ts - getter i setter, this jest wymagane w ts */
    set name(val) { this.ourName = val; }
    get name() { return this.ourName; }
}