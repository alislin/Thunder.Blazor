"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var buttons_1 = require("../options/buttons");
var modal_1 = require("./modal");
var markup_1 = require("../markup");
var state_1 = require("../state");
var actions_1 = require("../actions");
var class_list_1 = require("../class-list");
var CONTENT = class_list_1.default.CONTENT;
/*
 * Add an <input> to the content container.
 * Update the "promised" value of the confirm button whenever
 * the user types into the input (+ make it "" by default)
 * Set the default focus on the input.
 */
var addInputEvents = function (input) {
    input.addEventListener('input', function (e) {
        var target = e.target;
        var text = target.value;
        state_1.setActionValue(text);
    });
    input.addEventListener('keyup', function (e) {
        if (e.key === "Enter") {
            return actions_1.onAction(buttons_1.CONFIRM_KEY);
        }
    });
    /*
     * FIXME (this is a bit hacky)
     * We're overwriting the default value of confirm button,
     * as well as overwriting the default focus on the button
     */
    setTimeout(function () {
        input.focus();
        state_1.setActionValue('');
    }, 0);
};
var initPredefinedContent = function (content, elName, attrs) {
    var el = document.createElement(elName);
    var elClass = CONTENT + "__" + elName;
    el.classList.add(elClass);
    // Set things like "placeholder":
    for (var key in attrs) {
        var value = attrs[key];
        el[key] = value;
    }
    if (elName === "input") {
        addInputEvents(el);
    }
    content.appendChild(el);
};
var initContent = function (opts) {
    if (!opts)
        return;
    var content = modal_1.injectElIntoModal(markup_1.contentMarkup);
    var element = opts.element, attributes = opts.attributes;
    if (typeof element === "string") {
        initPredefinedContent(content, element, attributes);
    }
    else {
        content.appendChild(element);
    }
};
exports.default = initContent;
//# sourceMappingURL=content.js.map