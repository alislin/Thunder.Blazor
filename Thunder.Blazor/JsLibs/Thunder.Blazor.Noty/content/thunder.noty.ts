import NotyJs = require("./noty.min")
namespace Thunder.Noty {

    declare var window: Window & { ThunderBlazor: ThunderBlazor }

    class ThunderBlazor {}

    export function Init(): void {
        const obj = {
            Noty: new NotyWrap()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }

    class NotyWrap {
        public Show(data: NotyData, callback: any) {
            new NotyJs(data).show().on('onClick', () => {
                if (typeof callback === 'object') callback.invokeMethodAsync("CallAction", data);
            });
        }

        public CloseAll() {
            NotyJs.closeAll();
        }
        
    }

    class NotyData {
        text: string;
        timeout: number;
        layout: string;
        theme: string;
        type: string;
    }
}
Thunder.Noty.Init();