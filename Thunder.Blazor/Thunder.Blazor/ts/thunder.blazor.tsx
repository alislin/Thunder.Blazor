class thunder {
    thunder(): void {

    }


    animateCSS(element: string, animationName: string, callback: any): void {
        const node = document.querySelector(element);
        node.classList.add('animated', animationName);

        function handleAnimationEnd() {
            node.classList.remove('animated', animationName);
            node.removeEventListener('animationend', handleAnimationEnd);

            if (typeof callback === 'function') callback();
        }

        node.addEventListener('animationend', handleAnimationEnd);
    }
}