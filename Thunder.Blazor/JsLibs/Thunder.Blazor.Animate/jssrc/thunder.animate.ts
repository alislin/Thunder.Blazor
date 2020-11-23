namespace Thunder.Animate {
    class Animate {
        public Start(data: AnimateData,  callback: any): void {
            const node = document.querySelector('#' + data.id);
            if (node == null || node == undefined) {
                return;
            }

            function handleAnimationEnd() {
                node.removeEventListener('animationend', handleAnimationEnd);

                this.AddCss(node, data.endClass?.addCss)
                    .RemoveCss(node, data.endClass?.removeCss);

                if (typeof callback === 'object') callback.invokeMethodAsync("CallAction", data);
            }

            this.AddCss(node, data.beginClass?.addCss)
                .RemoveCss(node, data.beginClass?.removeCss);

            if (data.animateClass !== null || data.animateClass?.length > 0) {
                for (var i = 0; i < data.animateClass.length; i++) {
                    node.classList.add(data.animateClass[i]);
                }
                node.addEventListener('animationend', handleAnimationEnd);
            }
        }

        public Reset(data: AnimateData): void {
            const node = document.querySelector('#' + data.id);
            if (node == null || node == undefined) {
                return;
            }
            for (var i = 0; i < data.animateClass.length; i++) {
                node.classList.remove(data.animateClass[i]);
            }
        }

        public AddCss(node: Element, css: string[]): Animate {
            if (css !== undefined &&( css !== null || css?.length > 0)) {
                for (var i = 0; i < css.length; i++) {
                    node.classList.add(css[i]);
                }
            }
            return this;
        }

        public RemoveCss(node: Element, css: string[]): Animate {
            if (css !== undefined && (css !== null || css?.length > 0)) {
                for (var i = 0; i < css.length; i++) {
                    node.classList.remove(css[i]);
                }
            }
            return this;
        }
    }

    class AnimateData {
        public id: string;
        public animateClass: string[];
        public beginClass: MarkClass;
        public endClass: MarkClass;
    }

    class MarkClass {
        public addCss: string[];
        public removeCss: string[];
    }


    declare var window: Window & { ThunderBlazor: any }

    export function Init(): void {
        const obj = {
            Animate: new Animate()
        };
        
        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }
}

Thunder.Animate.Init();
