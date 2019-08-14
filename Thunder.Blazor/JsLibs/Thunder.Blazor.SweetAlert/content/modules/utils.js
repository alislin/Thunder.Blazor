"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/*
 * Get a DOM element from a class name:
 */
exports.getNode = function (className) {
    var selector = "." + className;
    return document.querySelector(selector);
};
exports.stringToNode = function (html) {
    var wrapper = document.createElement('div');
    wrapper.innerHTML = html.trim();
    return wrapper.firstChild;
};
exports.insertAfter = function (newNode, referenceNode) {
    var nextNode = referenceNode.nextSibling;
    var parentNode = referenceNode.parentNode;
    parentNode.insertBefore(newNode, nextNode);
};
exports.removeNode = function (node) {
    node.parentElement.removeChild(node);
};
exports.throwErr = function (message) {
    // Remove multiple spaces:
    message = message.replace(/ +(?= )/g, '');
    message = message.trim();
    throw "SweetAlert: " + message;
};
/*
 * Match plain objects ({}) but NOT null
 */
exports.isPlainObject = function (value) {
    if (Object.prototype.toString.call(value) !== '[object Object]') {
        return false;
    }
    else {
        var prototype = Object.getPrototypeOf(value);
        return prototype === null || prototype === Object.prototype;
    }
};
/*
 * Take a number and return a version with ordinal suffix
 * Example: 1 => 1st
 */
exports.ordinalSuffixOf = function (num) {
    var j = num % 10;
    var k = num % 100;
    if (j === 1 && k !== 11) {
        return num + "st";
    }
    if (j === 2 && k !== 12) {
        return num + "nd";
    }
    if (j === 3 && k !== 13) {
        return num + "rd";
    }
    return num + "th";
};
//# sourceMappingURL=utils.js.map