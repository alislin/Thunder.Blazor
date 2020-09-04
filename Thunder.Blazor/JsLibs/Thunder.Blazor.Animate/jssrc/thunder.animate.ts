namespace Thunder.Animate {
    class Animate {
        public Start(data: AnimateData,  callback: any): void {
            const node = document.querySelector('#' + data.id);
            if (node == null || node == undefined) {
                return;
            }

            function handleAnimationEnd() {
                node.removeEventListener('animationend', handleAnimationEnd);
                if (data.beginClass !== null || data.beginClass?.length > 0) {
                    for (var i = 0; i < data.beginClass.length; i++) {
                        node.classList.remove(data.beginClass[i]);
                    }
                }
                if (data.endClass !== null || data.endClass?.length > 0) {
                    for (var i = 0; i < data.endClass.length; i++) {
                        node.classList.add(data.endClass[i]);
                    }
                }

                if (typeof callback === 'object') callback.invokeMethodAsync("CallAction", data);
            }

            if (data.beginClass !== null || data.beginClass?.length > 0) {
                for (var i = 0; i < data.beginClass.length; i++) {
                    node.classList.add(data.beginClass[i]);
                }
            }
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
    }

    class AnimateData {
        public id: string;
        public animateClass: string[];
        public beginClass: string[];
        public endClass: string[];
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
