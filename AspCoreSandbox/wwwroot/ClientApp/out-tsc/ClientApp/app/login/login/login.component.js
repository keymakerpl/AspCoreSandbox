import { __decorate } from "tslib";
import { Component } from '@angular/core';
var LoginComponent = /** @class */ (function () {
    /** login ctor */
    function LoginComponent(data, router) {
        this.data = data;
        this.router = router;
        this.creds = { username: "", password: "" };
    }
    LoginComponent.prototype.onLogin = function () {
        var _this = this;
        this.data.login(this.creds).subscribe(function (success) {
            if (success) {
                if (_this.data.order.items.length == 0) {
                    _this.router.navigate([""]);
                }
                else {
                    _this.router.navigate(["checkout"]);
                }
            }
        }, function (error) { _this.errorMessage = "Failed to login"; });
    };
    LoginComponent = __decorate([
        Component({
            selector: 'app-login',
            templateUrl: './login.component.html',
            styleUrls: ['./login.component.scss']
        })
        /** login component*/
    ], LoginComponent);
    return LoginComponent;
}());
export { LoginComponent };
//# sourceMappingURL=login.component.js.map