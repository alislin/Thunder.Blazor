"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utils_1 = require("../utils");
var markup_1 = require("../markup");
var initOverlayOnce = function () {
    var overlay = utils_1.stringToNode(markup_1.overlayMarkup);
    document.body.appendChild(overlay);
};
exports.default = initOverlayOnce;
//# sourceMappingURL=overlay.js.map