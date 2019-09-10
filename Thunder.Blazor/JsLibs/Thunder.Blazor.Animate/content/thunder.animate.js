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
                var node = document.querySelector('#' + data.id);
                function handleAnimationEnd() {
                    node.removeEventListener('animationend', handleAnimationEnd);
                    if (typeof callback === 'object')
                        callback.invokeMethodAsync("CallAction", data);
                }
                if (data.animateClass !== null || data.animateClass.length > 0) {
                    for (var i = 0; i < data.animateClass.length; i++) {
                        node.classList.add(data.animateClass[i]);
                    }
                    node.addEventListener('animationend', handleAnimationEnd);
                }
            };
            Animate.prototype.Reset = function (data) {
                var node = document.querySelector('#' + data.id);
                for (var i = 0; i < data.animateClass.length; i++) {
                    node.classList.remove(data.animateClass[i]);
                }
            };
            return Animate;
        }());
        var AnimateData = /** @class */ (function () {
            function AnimateData() {
            }
            return AnimateData;
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