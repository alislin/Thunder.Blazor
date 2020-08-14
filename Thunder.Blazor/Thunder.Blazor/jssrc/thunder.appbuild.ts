namespace Thunder.AppBuilder {
    class CssBuild {
        public Add(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            if (node == null) {
                return;
            }
            if (data.list !== null || data.list.length > 0) {
                for (var i = 0; i < data.list.length; i++) {
                    node.classList.add(data.list[i]);
                }
            }
        }

        public Remove(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            if (node == null) {
                return;
            }
            for (var i = 0; i < data.list.length; i++) {
                node.classList.remove(data.list[i]);
            }
        }

        public ClassList(id: string): CssData {
            const node = document.querySelector('#' + id);
            var result = new CssData();
            result.id = id;
            result.list = new Array();
            if (node == null) {
                return result;
            }
            node.classList.forEach(
                function (value, key, obj) {
                    result.list.push(value);
                }
            )
            return result;
        }
    }

    class App {
        public getNavigator(): Navigator {
            var nav = new Navigator();
            nav.appCodeName = navigator.appCodeName;
            nav.appVersion = navigator.appVersion;
            nav.userAgent = navigator.userAgent;
            return nav;
            //return navigator;
        }
        public getScreent(): Screen { return window.screen; }
        public userAgent(): string { return navigator.userAgent; }
        public 
        public getTitle(): string { return document.title; }
        public setTitle(title: string) { document.title = title; }
    }

    class CssData {
        public id: string;
        public list: string[];
    }

    class Navigator {
        public appCodeName: string;
        public appName: string;
        public appVersion: string;
        public browserLanguage: string;
        public cookieEnabled: boolean;
        public cpuClass: string;
        public onLine: string;
        public platform: string;
        public systemLanguage: string;
        public userAgent: string;
        public userLanguage: string;
    }

    declare var window: Window & { ThunderBlazor: any }

    export function Init(): void {
        const obj = {
            CssBuilder: new CssBuild(),
            App: new App()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }
}
Thunder.AppBuilder.Init();