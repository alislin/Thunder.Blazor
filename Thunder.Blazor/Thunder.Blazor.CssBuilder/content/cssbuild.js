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
    var CssBuilder;
    (function (CssBuilder) {
        var CssBuild = /** @class */ (function () {
            function CssBuild() {
            }
            CssBuild.prototype.Add = function (data) {
                var node = document.querySelector('#' + data.id);
                if (data.list !== null || data.list.length > 0) {
                    for (var i = 0; i < data.list.length; i++) {
                        node.classList.add(data.list[i]);
                    }
                }
            };
            CssBuild.prototype.Remove = function (data) {
                var node = document.querySelector('#' + data.id);
                for (var i = 0; i < data.list.length; i++) {
                    node.classList.remove(data.list[i]);
                }
            };
            CssBuild.prototype.ClassList = function (id) {
                var node = document.querySelector('#' + id);
                var result = new CssData();
                result.id = id;
                result.list = new Array();
                //var iterator = node.classList.values();
                node.classList.forEach(function (value, key, obj) {
                    result.list.push(value);
                });
                //for (var value of iterator) {
                //    result.list.push(value);
                //}
                return result;
            };
            return CssBuild;
        }());
        var CssData = /** @class */ (function () {
            function CssData() {
            }
            return CssData;
        }());
        function Init() {
            var obj = {
                CssBuilder: new CssBuild()
            };
            if (window.ThunderBlazor) {
                window.ThunderBlazor = __assign({}, window.ThunderBlazor, obj);
            }
            else {
                window.ThunderBlazor = __assign({}, obj);
            }
        }
        CssBuilder.Init = Init;
    })(CssBuilder = Thunder.CssBuilder || (Thunder.CssBuilder = {}));
})(Thunder || (Thunder = {}));
Thunder.CssBuilder.Init();
//# sourceMappingURL=cssbuild.js.map