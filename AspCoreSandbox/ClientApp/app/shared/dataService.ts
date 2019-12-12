
import { HttpClient, HttpHeaders } from "@angular/common/http"; /** Do komunikacji z WEB API */
import { Injectable } from "@angular/core"; /** Do wstrzykiwania zależności, aby wstrzyknął HttpClient */
import { map } from "rxjs/operators"; /** Do komunikacji asynchronicznej pomiędzy koponentami w angularze */
import { Observable } from "rxjs"; /** Do */
import { Product } from './product';
import * as OrderNS from './order';


@Injectable()
export class DataService {

    constructor(private http: HttpClient) {

    }

    private token: string = "";
    private tokenExpireation: Date;

    public products: Product[] = [];
    public order: OrderNS.Order = new OrderNS.Order();

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpireation > new Date();
    }

    login(creds): Observable<boolean> {
        return this.http.post("/account/createtoken", creds).pipe(
            map((data: any) =>
            {
                this.token = data.token;
                this.tokenExpireation = data.expiration;
                return true;
            })); 
    }

    public checkout() {
        return this.http.post("/api/orders", this.order, { headers: new HttpHeaders().set("Authorization", "Bearer " + this.token) } )
            .pipe(map(response => { this.order = new OrderNS.Order(); return true; }));
    }

    public addToOrder(newProduct: Product) {

        var item: OrderNS.OrderItem = this.order.items.find(i => i.productId == newProduct.id);

        if (item != null) {
            item.quantity++;
        }
        else {
            var item: OrderNS.OrderItem = new OrderNS.OrderItem();
            item.productId = newProduct.id;
            item.unitPrice = newProduct.price;
            item.quantity = 1;

            this.order.items.push(item);
        }
    }

    /** Zwracamy boola po wczytaniu, poźniej subskrajber wczyta elementy na listę */
    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products").pipe(map((data: any[]) => { this.products = data; return true; }));
    }
}