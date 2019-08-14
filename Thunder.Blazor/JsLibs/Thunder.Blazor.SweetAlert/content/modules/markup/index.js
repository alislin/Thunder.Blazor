"use strict";
function __export(m) {
    for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
}
Object.defineProperty(exports, "__esModule", { value: true });
__export(require("./modal"));
var overlay_1 = require("./overlay");
exports.overlayMarkup = overlay_1.default;
__export(require("./icons"));
__export(require("./content"));
__export(require("./buttons"));
var class_list_1 = require("../class-list");
var MODAL_TITLE = class_list_1.default.MODAL_TITLE, MODAL_TEXT = class_list_1.default.MODAL_TEXT, ICON = class_list_1.default.ICON, FOOTER = class_list_1.default.FOOTER;
exports.iconMarkup = "\n  <div class=\"" + ICON + "\"></div>";
exports.titleMarkup = "\n  <div class=\"" + MODAL_TITLE + "\"></div>\n";
exports.textMarkup = "\n  <div class=\"" + MODAL_TEXT + "\"></div>";
exports.footerMarkup = "\n  <div class=\"" + FOOTER + "\"></div>\n";
//# sourceMappingURL=index.js.map