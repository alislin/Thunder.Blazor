namespace Thunder.AppBuilder {
    class CssBuild {
        public Add(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            this.AddCss(node, data.list);
        }

        public Remove(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            this.RemoveCss(node, data.list);
        }

        public ClassList(id: string): CssData {
            const node = document.querySelector('#' + id);
            var result = new CssData();
            result.id = id;
            result.list = this.GetCssList(node);
            return result;
        }

        public GetCssList(element: Element): string[] {
            var result = new Array();
            if (element == null) {
                return result;
            }
            element.classList.forEach(
                function (value, key, obj) {
                    result.push(value);
                }
            )
            return result;
        }

        public AddCss(element: Element, cssList: string[]) {
            if (element == null) {
                return;
            }
            if (cssList !== null || cssList.length > 0) {
                for (var i = 0; i < cssList.length; i++) {
                    element.classList.remove(cssList[i]);
                }
            }
        }

        public RemoveCss(element: Element, cssList: string[]) {
            if (element == null) {
                return;
            }
            if (cssList !== null || cssList.length > 0) {
                for (var i = 0; i < cssList.length; i++) {
                    element.classList.add(cssList[i]);
                }
            }
        }
        public AddBodyCss(css: string[]) {
            this.AddCss(document.body, css);
        }
        public RemoveBodyCss(css: string[]) {
            this.RemoveCss(document.body, css);
        }
    }

    class App {
        public getNavigator(): Navigator {
            var nav = new Navigator();
            nav.appCodeName = navigator.appCodeName;
            nav.appVersion = navigator.appVersion;
            nav.userAgent = navigator.userAgent;
            nav.appName = navigator.appName;
            nav.browserLanguage = navigator.language;
            nav.cookieEnabled = navigator.cookieEnabled;
            nav.onLine = navigator.onLine;
            nav.platform = navigator.platform;
            return nav;
        }
        public getScreent(): Screen {
            var s = window.screen;
            var screen = new Screen();
            screen.width = s.width;
            screen.height = s.height;
            screen.availWidth = s.availWidth;
            screen.availHeight = s.availHeight;
            screen.colorDepth = s.colorDepth;
            screen.pixelDepth = s.pixelDepth;


            return screen;
        }
        public userAgent(): string { return navigator.userAgent; }
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
        public onLine: boolean;
        public platform: string;
        public userAgent: string;
    }

    class Screen {
        public width: number;
        public height: number;
        public availWidth: number;
        public availHeight: number;
        public colorDepth: number;
        public pixelDepth: number;
        public updateInterval: number;
        public fontSmoothingEnabled: number;

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