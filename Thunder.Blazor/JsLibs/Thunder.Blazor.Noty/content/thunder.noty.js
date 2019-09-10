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
///<reference path = 'index.d.ts' />
var Thunder;
(function (Thunder) {
    var NotyJs;
    (function (NotyJs) {
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
                window.ThunderBlazor = __assign(__assign({}, window.ThunderBlazor), obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        NotyJs.Init = Init;
        var NotyWrap = /** @class */ (function () {
            function NotyWrap() {
            }
            NotyWrap.prototype.Show = function (data, callback) {
                var option = data;
                var noty = new Noty(data);
                noty.on('onClose', function () {
                    if (typeof callback === 'object')
                        callback.invokeMethodAsync("CallAction", data);
                });
                noty.show();
            };
            NotyWrap.prototype.CloseAll = function () {
                Noty.closeAll();
            };
            return NotyWrap;
        }());
        var NotyData = /** @class */ (function () {
            function NotyData() {
            }
            return NotyData;
        }());
    })(NotyJs = Thunder.NotyJs || (Thunder.NotyJs = {}));
})(Thunder || (Thunder = {}));
Thunder.NotyJs.Init();
