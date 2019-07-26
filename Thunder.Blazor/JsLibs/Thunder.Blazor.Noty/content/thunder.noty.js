"use strict";
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
Object.defineProperty(exports, "__esModule", { value: true });
var NotyJs = require("./noty.min");
var Thunder;
(function (Thunder) {
    var Noty;
    (function (Noty) {
        var ThunderBlazor = /** @class */ (function () {
            function ThunderBlazor() {
            }
            return ThunderBlazor;
        }());
        function Init() {
            var obj = {
                Noty: new NotyWrap()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign({}, window.ThunderBlazor, obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        Noty.Init = Init;
        var NotyWrap = /** @class */ (function () {
            function NotyWrap() {
            }
            NotyWrap.prototype.Show = function (data, callback) {
                new NotyJs(data).show().on('onClick', function () {
                    if (typeof callback === 'object')
                        callback.invokeMethodAsync("CallAction", data);
                });
            };
            NotyWrap.prototype.CloseAll = function () {
                NotyJs.closeAll();
            };
            return NotyWrap;
        }());
        var NotyData = /** @class */ (function () {
            function NotyData() {
            }
            return NotyData;
        }());
    })(Noty = Thunder.Noty || (Thunder.Noty = {}));
})(Thunder || (Thunder = {}));
Thunder.Noty.Init();
