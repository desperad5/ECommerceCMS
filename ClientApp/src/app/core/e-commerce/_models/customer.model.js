"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CustomerModel = /** @class */ (function () {
    function CustomerModel() {
    }
    CustomerModel.prototype.clear = function () {
        this.dob = new Date();
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.userName = '';
        this.gender = 'Female';
        this.ipAddress = '';
        this.type = 1;
        this.status = 1;
    };
    return CustomerModel;
}());
exports.CustomerModel = CustomerModel;
//# sourceMappingURL=customer.model.js.map