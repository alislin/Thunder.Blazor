namespace Thunder.DomObject {

    declare var window: Window & { ThunderBlazor: ThunderBlazor }

    class ThunderBlazor {

    }

    class BlazorCallback {
        Callback: string = "CallAction";
        Confirm: string = "ConfirmCallback";
        Cancel: string = "CancelCallback";
    }

    export function Init(): void {
        const obj = {
            Callback: new BlazorCallback()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }
}
Thunder.DomObject.Init();