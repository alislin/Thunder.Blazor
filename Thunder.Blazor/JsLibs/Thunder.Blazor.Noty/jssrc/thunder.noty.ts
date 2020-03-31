///<reference path = 'index.d.ts' />
namespace Thunder.NotyJs {

    declare var window: Window & { ThunderBlazor: ThunderBlazor }

    class ThunderBlazor { }

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

    class NotyData implements Noty.Options {
        text?: string;
        timeout?: number;
        layout?: Noty.Layout;
        theme?: Noty.Theme;
        type?: Noty.Type;
    }
}
Thunder.NotyJs.Init();
