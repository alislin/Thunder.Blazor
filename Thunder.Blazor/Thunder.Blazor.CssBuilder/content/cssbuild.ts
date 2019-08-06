namespace Thunder.CssBuilder {
    class CssBuild {
        public Add(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            if (data.list !== null || data.list.length > 0) {
                for (var i = 0; i < data.list.length; i++) {
                    node.classList.add(data.list[i]);
                }
            }
        }

        public Remove(data: CssData): void {
            const node = document.querySelector('#' + data.id);
            for (var i = 0; i < data.list.length; i++) {
                node.classList.remove(data.list[i]);
            }
        }

        public ClassList(id: string): CssData {
            const node = document.querySelector('#' + id);
            var result = new CssData();
            result.id = id;
            result.list = new Array();
            //var iterator = node.classList.values();
            node.classList.forEach(
                function (value, key, obj) {
                    result.list.push(value);
                }
            )
            //for (var value of iterator) {
            //    result.list.push(value);
            //}
            return result;
        }
    }

    class CssData {
        public id: string;
        public list: string[];
    }

    declare var window: Window & { ThunderBlazor: any }

    export function Init(): void {
        const obj = {
            CssBuilder: new CssBuild()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }
}
Thunder.CssBuilder.Init();