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
    var AnimateTransition;
    (function (AnimateTransition_1) {
        var AnimateTransition = /** @class */ (function () {
            function AnimateTransition() {
            }
            AnimateTransition.prototype.Start = function (data, callback) {
                var _a;
                var node = document.querySelector('#' + data.id);
                if (node == null || node == undefined) {
                    return;
                }
                function handleAnimationEnd() {
                    node.removeEventListener('animationend', handleAnimationEnd);
                    if (typeof callback === 'object')
                        callback.invokeMethodAsync("CallAction", data);
                }
                if (data.animateClass !== null || ((_a = data.animateClass) === null || _a === void 0 ? void 0 : _a.length) > 0) {
                    for (var i = 0; i < data.animateClass.length; i++) {
                        node.classList.add(data.animateClass[i]);
                    }
                    node.addEventListener('animationend', handleAnimationEnd);
                }
            };
            AnimateTransition.prototype.Reset = function (data) {
                var node = document.querySelector('#' + data.id);
                if (node == null || node == undefined) {
                    return;
                }
                for (var i = 0; i < data.animateClass.length; i++) {
                    node.classList.remove(data.animateClass[i]);
                }
            };
            return AnimateTransition;
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
                AnimateTransition: new AnimateTransition()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign(__assign({}, window.ThunderBlazor), obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        AnimateTransition_1.Init = Init;
    })(AnimateTransition = Thunder.AnimateTransition || (Thunder.AnimateTransition = {}));
})(Thunder || (Thunder = {}));
Thunder.AnimateTransition.Init();
//# sourceMappingURL=thunder.animateTransition.js.map