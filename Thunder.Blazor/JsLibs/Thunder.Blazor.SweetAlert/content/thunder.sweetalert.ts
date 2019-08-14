///<reference path = 'sweetalert.d.ts' />
namespace Thunder.Alert {

    declare var window: Window & { ThunderBlazor: ThunderBlazor }

    class ThunderBlazor {}

    export function Init(): void {
        const obj = {
            Noty: new AlertWrap()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }

    class AlertWrap {
        public Show(data: NotyData, callback: any) {
            var option = data;
            var noty = new Noty(data);
            noty.on('onClose', () => {
                if (typeof callback === 'object') callback.invokeMethodAsync("CallAction", data);
            });
            noty.show();
        }

        public CloseAll() {
            Noty.closeAll();
        }
        
    }

    class NotyData implements  Noty.Options {
        text?: string;
        timeout?: number;
        layout?: Noty.Layout;
        theme?: Noty.Theme;
        type?: Noty.Type;
    }
}
Thunder.Alert.Init();