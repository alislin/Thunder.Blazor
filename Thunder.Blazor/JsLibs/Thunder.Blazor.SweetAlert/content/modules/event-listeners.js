"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var state_1 = require("./state");
var actions_1 = require("./actions");
var utils_1 = require("./utils");
var buttons_1 = require("./options/buttons");
var class_list_1 = require("./class-list");
var MODAL = class_list_1.default.MODAL, BUTTON = class_list_1.default.BUTTON, OVERLAY = class_list_1.default.OVERLAY;
var onTabAwayLastButton = function (e) {
    e.preventDefault();
    setFirstButtonFocus();
};
var onTabBackFirstButton = function (e) {
    e.preventDefault();
    setLastButtonFocus();
};
var onKeyUp = function (e) {
    if (!state_1.default.isOpen)
        return;
    switch (e.key) {
        case "Escape": return actions_1.onAction(buttons_1.CANCEL_KEY);
    }
};
var onKeyDownLastButton = function (e) {
    if (!state_1.default.isOpen)
        return;
    switch (e.key) {
        case "Tab": return onTabAwayLastButton(e);
    }
};
var onKeyDownFirstButton = function (e) {
    if (!state_1.default.isOpen)
        return;
    if (e.key === "Tab" && e.shiftKey) {
        return onTabBackFirstButton(e);
    }
};
/*
 * Set default focus on Confirm-button
 */
var setFirstButtonFocus = function () {
    var button = utils_1.getNode(BUTTON);
    if (button) {
        button.tabIndex = 0;
        button.focus();
    }
};
var setLastButtonFocus = function () {
    var modal = utils_1.getNode(MODAL);
    var buttons = modal.querySelectorAll("." + BUTTON);
    var lastIndex = buttons.length - 1;
    var lastButton = buttons[lastIndex];
    if (lastButton) {
        lastButton.focus();
    }
};
var setTabbingForLastButton = function (buttons) {
    var lastIndex = buttons.length - 1;
    var lastButton = buttons[lastIndex];
    lastButton.addEventListener('keydown', onKeyDownLastButton);
};
var setTabbingForFirstButton = function (buttons) {
    var firstButton = buttons[0];
    firstButton.addEventListener('keydown', onKeyDownFirstButton);
};
var setButtonTabbing = function () {
    var modal = utils_1.getNode(MODAL);
    var buttons = modal.querySelectorAll("." + BUTTON);
    if (!buttons.length)
        return;
    setTabbingForLastButton(buttons);
    setTabbingForFirstButton(buttons);
};
var onOutsideClick = function (e) {
    var overlay = utils_1.getNode(OVERLAY);
    // Don't trigger for children:
    if (overlay !== e.target)
        return;
    return actions_1.onAction(buttons_1.CANCEL_KEY);
};
var setClickOutside = function (allow) {
    var overlay = utils_1.getNode(OVERLAY);
    overlay.removeEventListener('click', onOutsideClick);
    if (allow) {
        overlay.addEventListener('click', onOutsideClick);
    }
};
var setTimer = function (ms) {
    if (state_1.default.timer) {
        clearTimeout(state_1.default.timer);
    }
    if (ms) {
        state_1.default.timer = window.setTimeout(function () {
            return actions_1.onAction(buttons_1.CANCEL_KEY);
        }, ms);
    }
};
var addEventListeners = function (opts) {
    if (opts.closeOnEsc) {
        document.addEventListener('keyup', onKeyUp);
    }
    else {
        document.removeEventListener('keyup', onKeyUp);
    }
    /* So that you don't accidentally confirm something
     * dangerous by clicking enter
     */
    if (opts.dangerMode) {
        setFirstButtonFocus();
    }
    else {
        setLastButtonFocus();
    }
    setButtonTabbing();
    setClickOutside(opts.closeOnClickOutside);
    setTimer(opts.timer);
};
exports.default = addEventListeners;
//# sourceMappingURL=event-listeners.js.map