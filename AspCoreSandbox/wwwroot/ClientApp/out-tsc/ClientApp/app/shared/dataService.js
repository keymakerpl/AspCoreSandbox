import { __decorate } from "tslib";
import { HttpHeaders } from "@angular/common/http"; /** Do komunikacji z WEB API */
import { Injectable } from "@angular/core"; /** Do wstrzykiwania zależności, aby wstrzyknął HttpClient */
import { map } from "rxjs/operators"; /** Do komunikacji asynchronicznej pomiędzy koponentami w angularze */
import * as OrderNS from './order';
var DataService = /** @class */ (function () {
    function DataService(http) {
        this.http = http;
        this.token = "";
        this.products = [];
        this.order = new OrderNS.Order();
    }
    Object.defineProperty(DataService.prototype, "loginRequired", {
        get: function () {
            return this.token.length == 0 || this.tokenExpireation > new Date();
        },
        enumerable: true,
        configurable: true
    });
    DataService.prototype.login = function (creds) {
        var _this = this;
        return this.http.post("/account/createtoken", creds).pipe(map(function (data) {
            _this.token = data.token;
            _this.tokenExpireation = data.expiration;
            return true;
        }));
    };
    DataService.prototype.checkout = function () {
        var _this = this;
        return this.http.post("/api/orders", this.order, { headers: new HttpHeaders().set("Authorization", "Bearer " + this.token) })
            .pipe(map(function (response) { _this.order = new OrderNS.Order(); return true; }));
    };
    DataService.prototype.addToOrder = function (newProduct) {
        var item = this.order.items.find(function (i) { return i.productId == newProduct.id; });
        if (item != null) {
            item.quantity++;
        }
        else {
            var item = new OrderNS.OrderItem();
            item.productId = newProduct.id;
            item.unitPrice = newProduct.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    };
    /** Zwracamy boola po wczytaniu, poźniej subskrajber wczyta elementy na listę */
    DataService.prototype.loadProducts = function () {
        var _this = this;
        return this.http.get("/api/products").pipe(map(function (data) { _this.products = data; return true; }));
    };
    DataService = __decorate([
        Injectable()
    ], DataService);
    return DataService;
}());
export { DataService };
//# sourceMappingURL=dataService.js.map