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
    var AppBuilder;
    (function (AppBuilder) {
        var CssBuild = /** @class */ (function () {
            function CssBuild() {
            }
            CssBuild.prototype.Add = function (data) {
                var node = document.querySelector('#' + data.id);
                if (node == null) {
                    return;
                }
                if (data.list !== null || data.list.length > 0) {
                    for (var i = 0; i < data.list.length; i++) {
                        node.classList.add(data.list[i]);
                    }
                }
            };
            CssBuild.prototype.Remove = function (data) {
                var node = document.querySelector('#' + data.id);
                if (node == null) {
                    return;
                }
                for (var i = 0; i < data.list.length; i++) {
                    node.classList.remove(data.list[i]);
                }
            };
            CssBuild.prototype.ClassList = function (id) {
                var node = document.querySelector('#' + id);
                var result = new CssData();
                result.id = id;
                result.list = new Array();
                if (node == null) {
                    return result;
                }
                node.classList.forEach(function (value, key, obj) {
                    result.list.push(value);
                });
                return result;
            };
            return CssBuild;
        }());
        var App = /** @class */ (function () {
            function App() {
            }
            App.prototype.getNavigator = function () {
                var nav = new Navigator();
                nav.appCodeName = navigator.appCodeName;
                nav.appVersion = navigator.appVersion;
                nav.userAgent = navigator.userAgent;
                return nav;
                //return navigator;
            };
            App.prototype.getScreent = function () { return window.screen; };
            App.prototype.userAgent = function () { return navigator.userAgent; };
            App.prototype.getTitle = function () { return document.title; };
            App.prototype.setTitle = function (title) { document.title = title; };
            return App;
        }());
        var CssData = /** @class */ (function () {
            function CssData() {
            }
            return CssData;
        }());
        var Navigator = /** @class */ (function () {
            function Navigator() {
            }
            return Navigator;
        }());
        function Init() {
            var obj = {
                CssBuilder: new CssBuild(),
                App: new App()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign(__assign({}, window.ThunderBlazor), obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        AppBuilder.Init = Init;
    })(AppBuilder = Thunder.AppBuilder || (Thunder.AppBuilder = {}));
})(Thunder || (Thunder = {}));
Thunder.AppBuilder.Init();
//# sourceMappingURL=thunder.appbuild.js.map