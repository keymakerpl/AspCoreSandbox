import { __decorate } from "tslib";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { DataService } from './shared/dataService';
import { Cart } from './shop/cart.component';
import { Shop } from './shop/shop.component';
import { Checkout } from './checkout/checkout.component';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login/login.component';
import { FormsModule } from "@angular/forms";
var routes = [{ path: "", component: Shop }, { path: "checkout", component: Checkout }, { path: "login", component: LoginComponent }];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                ProductList,
                Cart,
                Shop,
                Checkout,
                LoginComponent
            ],
            imports: [
                BrowserModule, HttpClientModule, RouterModule.forRoot(routes, { useHash: true, enableTracing: false }), FormsModule
            ],
            providers: [DataService],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map