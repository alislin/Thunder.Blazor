"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utils_1 = require("../utils");
var markup_1 = require("../markup");
var class_list_1 = require("../class-list");
var MODAL = class_list_1.default.MODAL, OVERLAY = class_list_1.default.OVERLAY;
var icon_1 = require("./icon");
var text_1 = require("./text");
var buttons_1 = require("./buttons");
var content_1 = require("./content");
exports.injectElIntoModal = function (markup) {
    var modal = utils_1.getNode(MODAL);
    var el = utils_1.stringToNode(markup);
    modal.appendChild(el);
    return el;
};
/*
 * Remove eventual added classes +
 * reset all content inside:
 */
var resetModalElement = function (modal) {
    modal.className = MODAL;
    modal.textContent = '';
};
/*
 * Add custom class to modal element
 */
var customizeModalElement = function (modal, opts) {
    resetModalElement(modal);
    var className = opts.className;
    if (className) {
        modal.classList.add(className);
    }
};
/*
 * It's important to run the following functions in this particular order,
 * so that the elements get appended one after the other.
 */
exports.initModalContent = function (opts) {
    // Start from scratch:
    var modal = utils_1.getNode(MODAL);
    customizeModalElement(modal, opts);
    icon_1.default(opts.icon);
    text_1.initTitle(opts.title);
    text_1.initText(opts.text);
    content_1.default(opts.content);
    buttons_1.default(opts.buttons, opts.dangerMode);
};
var initModalOnce = function () {
    var overlay = utils_1.getNode(OVERLAY);
    var modal = utils_1.stringToNode(markup_1.modalMarkup);
    overlay.appendChild(modal);
};
exports.default = initModalOnce;
//# sourceMappingURL=modal.js.map