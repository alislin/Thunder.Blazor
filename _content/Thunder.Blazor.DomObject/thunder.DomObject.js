var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var Thunder;
(function (Thunder) {
    var DomObject;
    (function (DomObject) {
        var ThunderBlazor = /** @class */ (function () {
            function ThunderBlazor() {
            }
            return ThunderBlazor;
        }());
        var BlazorCallback = /** @class */ (function () {
            function BlazorCallback() {
                this.Callback = "CallAction";
                this.Confirm = "ConfirmCallback";
                this.Cancel = "CancelCallback";
            }
            return BlazorCallback;
        }());
        function Init() {
            var obj = {
                Callback: new BlazorCallback()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign({}, window.ThunderBlazor, obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        DomObject.Init = Init;
    })(DomObject = Thunder.DomObject || (Thunder.DomObject = {}));
})(Thunder || (Thunder = {}));
Thunder.DomObject.Init();
//# sourceMappingURL=thunder.DomObject.js.map