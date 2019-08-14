"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utils_1 = require("../utils");
var class_list_1 = require("../class-list");
var MODAL = class_list_1.default.MODAL;
var modal_1 = require("./modal");
var overlay_1 = require("./overlay");
var event_listeners_1 = require("../event-listeners");
var utils_2 = require("../utils");
/*
 * Inject modal and overlay into the DOM
 * Then format the modal according to the given opts
 */
exports.init = function (opts) {
    var modal = utils_1.getNode(MODAL);
    if (!modal) {
        if (!document.body) {
            utils_2.throwErr("You can only use SweetAlert AFTER the DOM has loaded!");
        }
        overlay_1.default();
        modal_1.default();
    }
    modal_1.initModalContent(opts);
    event_listeners_1.default(opts);
};
exports.default = exports.init;
//# sourceMappingURL=index.js.map