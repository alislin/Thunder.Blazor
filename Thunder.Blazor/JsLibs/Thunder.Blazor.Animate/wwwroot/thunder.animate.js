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
    var Animate;
    (function (Animate_1) {
        var Animate = /** @class */ (function () {
            function Animate() {
            }
            Animate.prototype.Start = function (data, callback) {
                var _a, _b, _c;
                var node = document.querySelector('#' + data.id);
                if (node == null || node == undefined) {
                    return;
                }
                function handleAnimationEnd() {
                    var _a, _b;
                    node.removeEventListener('animationend', handleAnimationEnd);
                    this.AddCss(node, (_a = data.endClass) === null || _a === void 0 ? void 0 : _a.addCss)
                        .RemoveCss(node, (_b = data.endClass) === null || _b === void 0 ? void 0 : _b.removeCss);
                    if (typeof callback === 'object')
                        callback.invokeMethodAsync("CallAction", data);
                }
                this.AddCss(node, (_a = data.beginClass) === null || _a === void 0 ? void 0 : _a.addCss)
                    .RemoveCss(node, (_b = data.beginClass) === null || _b === void 0 ? void 0 : _b.removeCss);
                if (data.animateClass !== null || ((_c = data.animateClass) === null || _c === void 0 ? void 0 : _c.length) > 0) {
                    for (var i = 0; i < data.animateClass.length; i++) {
                        node.classList.add(data.animateClass[i]);
                    }
                    node.addEventListener('animationend', handleAnimationEnd);
                }
            };
            Animate.prototype.Reset = function (data) {
                var node = document.querySelector('#' + data.id);
                if (node == null || node == undefined) {
                    return;
                }
                for (var i = 0; i < data.animateClass.length; i++) {
                    node.classList.remove(data.animateClass[i]);
                }
            };
            Animate.prototype.AddCss = function (node, css) {
                if (css !== null || (css === null || css === void 0 ? void 0 : css.length) > 0) {
                    for (var i = 0; i < css.length; i++) {
                        node.classList.add(css[i]);
                    }
                }
                return this;
            };
            Animate.prototype.RemoveCss = function (node, css) {
                if (css !== null || (css === null || css === void 0 ? void 0 : css.length) > 0) {
                    for (var i = 0; i < css.length; i++) {
                        node.classList.remove(css[i]);
                    }
                }
                return this;
            };
            return Animate;
        }());
        var AnimateData = /** @class */ (function () {
            function AnimateData() {
            }
            return AnimateData;
        }());
        var MarkClass = /** @class */ (function () {
            function MarkClass() {
            }
            return MarkClass;
        }());
        function Init() {
            var obj = {
                Animate: new Animate()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign(__assign({}, window.ThunderBlazor), obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        Animate_1.Init = Init;
    })(Animate = Thunder.Animate || (Thunder.Animate = {}));
})(Thunder || (Thunder = {}));
Thunder.Animate.Init();
//# sourceMappingURL=thunder.animate.js.map