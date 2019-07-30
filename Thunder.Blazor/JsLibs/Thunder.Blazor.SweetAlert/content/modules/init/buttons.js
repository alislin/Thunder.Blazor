"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utils_1 = require("../utils");
var modal_1 = require("./modal");
var class_list_1 = require("../class-list");
var BUTTON = class_list_1.default.BUTTON, DANGER_BUTTON = class_list_1.default.DANGER_BUTTON;
var buttons_1 = require("../options/buttons");
var markup_1 = require("../markup");
var actions_1 = require("../actions");
var state_1 = require("../state");
/*
 * Generate a button, with a container element,
 * the right class names, the text, and an event listener.
 * IMPORTANT: This will also add the button's action, which can be triggered even if the button element itself isn't added to the modal.
 */
var getButton = function (namespace, _a, dangerMode) {
    var text = _a.text, value = _a.value, className = _a.className, closeModal = _a.closeModal;
    var buttonContainer = utils_1.stringToNode(markup_1.buttonMarkup);
    var buttonEl = buttonContainer.querySelector("." + BUTTON);
    var btnNamespaceClass = BUTTON + "--" + namespace;
    buttonEl.classList.add(btnNamespaceClass);
    if (className) {
        var classNameArray = Array.isArray(className)
            ? className
            : className.split(' ');
        classNameArray
            .filter(function (name) { return name.length > 0; })
            .forEach(function (name) {
            buttonEl.classList.add(name);
        });
    }
    if (dangerMode && namespace === buttons_1.CONFIRM_KEY) {
        buttonEl.classList.add(DANGER_BUTTON);
    }
    buttonEl.textContent = text;
    var actionValues = {};
    actionValues[namespace] = value;
    state_1.setActionValue(actionValues);
    state_1.setActionOptionsFor(namespace, {
        closeModal: closeModal,
    });
    buttonEl.addEventListener('click', function () {
        return actions_1.onAction(namespace);
    });
    return buttonContainer;
};
/*
 * Create the buttons-container,
 * then loop through the ButtonList object
 * and append every button to it.
 */
var initButtons = function (buttons, dangerMode) {
    var footerEl = modal_1.injectElIntoModal(markup_1.footerMarkup);
    for (var key in buttons) {
        var buttonOpts = buttons[key];
        var buttonEl = getButton(key, buttonOpts, dangerMode);
        if (buttonOpts.visible) {
            footerEl.appendChild(buttonEl);
        }
    }
    /*
     * If the footer has no buttons, there's no
     * point in keeping it:
     */
    if (footerEl.children.length === 0) {
        footerEl.remove();
    }
};
exports.default = initButtons;
//# sourceMappingURL=buttons.js.map