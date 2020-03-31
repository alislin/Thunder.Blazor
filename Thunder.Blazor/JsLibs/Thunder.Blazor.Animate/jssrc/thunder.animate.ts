namespace Thunder.Animate {
    class Animate {
        public Start(data: AnimateData,  callback: any): void {
            const node = document.querySelector('#' + data.id);

            function handleAnimationEnd() {
                node.removeEventListener('animationend', handleAnimationEnd);

                if (typeof callback === 'object') callback.invokeMethodAsync("CallAction", data);
            }

            if (data.animateClass !== null || data.animateClass.length > 0) {
                for (var i = 0; i < data.animateClass.length; i++) {
                    node.classList.add(data.animateClass[i]);
                }
                node.addEventListener('animationend', handleAnimationEnd);
            }
        }

        public Reset(data: AnimateData): void {
            const node = document.querySelector('#' + data.id);
            for (var i = 0; i < data.animateClass.length; i++) {
                node.classList.remove(data.animateClass[i]);
            }
        }
    }

    class AnimateData {
        public id: string;
        public animateClass: string[];
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
