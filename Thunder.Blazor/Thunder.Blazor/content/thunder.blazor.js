var thunder = function () {
    return {
        animateCSS: function (data, callback) {
            const node = document.querySelector(data.id);
            node.classList.add('animated', data.type);

            function handleAnimationEnd() {
                node.classList.remove('animated', data.type);
                node.removeEventListener('animationend', handleAnimationEnd);

                if (typeof callback === 'function') callback.invokeMethodAsync("CallAction");
            }

            node.addEventListener('animationend', handleAnimationEnd);
        }
    };
};
