"use strict";
/*
 * A list of all the deprecated options from SweetAlert 1.X
 * These should log a warning telling users how to upgrade.
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.logDeprecation = function (name) {
    var details = exports.DEPRECATED_OPTS[name];
    var onlyRename = details.onlyRename, replacement = details.replacement, subOption = details.subOption, link = details.link;
    var destiny = (onlyRename) ? 'renamed' : 'deprecated';
    var message = "SweetAlert warning: \"" + name + "\" option has been " + destiny + ".";
    if (replacement) {
        var subOptionText = (subOption) ? " \"" + subOption + "\" in " : ' ';
        message += " Please use" + subOptionText + "\"" + replacement + "\" instead.";
    }
    var DOMAIN = 'https://sweetalert.js.org';
    if (link) {
        message += " More details: " + DOMAIN + link;
    }
    else {
        message += " More details: " + DOMAIN + "/guides/#upgrading-from-1x";
    }
    console.warn(message);
};
;
;
exports.DEPRECATED_OPTS = {
    'type': {
        replacement: 'icon',
        link: '/docs/#icon',
    },
    'imageUrl': {
        replacement: 'icon',
        link: '/docs/#icon',
    },
    'customClass': {
        replacement: 'className',
        onlyRename: true,
        link: '/docs/#classname',
    },
    'imageSize': {},
    'showCancelButton': {
        replacement: 'buttons',
        link: '/docs/#buttons',
    },
    'showConfirmButton': {
        replacement: 'button',
        link: '/docs/#button',
    },
    'confirmButtonText': {
        replacement: 'button',
        link: '/docs/#button',
    },
    'confirmButtonColor': {},
    'cancelButtonText': {
        replacement: 'buttons',
        link: '/docs/#buttons',
    },
    'closeOnConfirm': {
        replacement: 'button',
        subOption: 'closeModal',
        link: '/docs/#button',
    },
    'closeOnCancel': {
        replacement: 'buttons',
        subOption: 'closeModal',
        link: '/docs/#buttons',
    },
    'showLoaderOnConfirm': {
        replacement: 'buttons',
    },
    'animation': {},
    'inputType': {
        replacement: 'content',
        link: '/docs/#content',
    },
    'inputValue': {
        replacement: 'content',
        link: '/docs/#content',
    },
    'inputPlaceholder': {
        replacement: 'content',
        link: '/docs/#content',
    },
    'html': {
        replacement: 'content',
        link: '/docs/#content',
    },
    'allowEscapeKey': {
        replacement: 'closeOnEsc',
        onlyRename: true,
        link: '/docs/#closeonesc',
    },
    'allowClickOutside': {
        replacement: 'closeOnClickOutside',
        onlyRename: true,
        link: '/docs/#closeonclickoutside',
    },
};
//# sourceMappingURL=deprecations.js.map