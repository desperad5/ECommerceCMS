"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AuthInterceptor = /** @class */ (function () {
    function AuthInterceptor() {
    }
    AuthInterceptor.prototype.intercept = function (req, next) {
         
        var authToken = localStorage.getItem("authToken");
        if (authToken) {
            var cloned = req.clone({
                headers: req.headers.set("Authorization", "Bearer " + authToken)
            });
            return next.handle(cloned);
        }
        else {
            return next.handle(req);
        }
    };
    return AuthInterceptor;
}());
exports.AuthInterceptor = AuthInterceptor;
//# sourceMappingURL=auth.interceptor.js.map
