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
                this.AddCss(node, data.list);
            };
            CssBuild.prototype.Remove = function (data) {
                var node = document.querySelector('#' + data.id);
                this.RemoveCss(node, data.list);
            };
            CssBuild.prototype.ClassList = function (id) {
                var node = document.querySelector('#' + id);
                var result = new CssData();
                result.id = id;
                result.list = this.GetCssList(node);
                return result;
            };
            CssBuild.prototype.GetCssList = function (element) {
                var result = new Array();
                if (element == null) {
                    return result;
                }
                element.classList.forEach(function (value, key, obj) {
                    result.push(value);
                });
                return result;
            };
            CssBuild.prototype.AddCss = function (element, cssList) {
                if (element == null) {
                    return;
                }
                if (cssList !== null || cssList.length > 0) {
                    for (var i = 0; i < cssList.length; i++) {
                        element.classList.remove(cssList[i]);
                    }
                }
            };
            CssBuild.prototype.RemoveCss = function (element, cssList) {
                if (element == null) {
                    return;
                }
                if (cssList !== null || cssList.length > 0) {
                    for (var i = 0; i < cssList.length; i++) {
                        element.classList.add(cssList[i]);
                    }
                }
            };
            CssBuild.prototype.AddBodyCss = function (css) {
                this.AddCss(document.body, css);
            };
            CssBuild.prototype.RemoveBodyCss = function (css) {
                this.RemoveCss(document.body, css);
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
                nav.appName = navigator.appName;
                nav.browserLanguage = navigator.language;
                nav.cookieEnabled = navigator.cookieEnabled;
                nav.onLine = navigator.onLine;
                nav.platform = navigator.platform;
                return nav;
            };
            App.prototype.getScreent = function () {
                var s = window.screen;
                var screen = new Screen();
                screen.width = s.width;
                screen.height = s.height;
                screen.availWidth = s.availWidth;
                screen.availHeight = s.availHeight;
                screen.colorDepth = s.colorDepth;
                screen.pixelDepth = s.pixelDepth;
                return screen;
            };
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
        var Screen = /** @class */ (function () {
            function Screen() {
            }
            return Screen;
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