declare namespace CustomSolutions {
    namespace Cards {
        function bindEvents(selectedIndustry: number, isGeneralSelected: boolean, minCriticals: number, maxCriticals: number, maxNice: number): void;
        namespace Tests {
            function runTests(): boolean;
        }
    }
    namespace RadioButtons {
        function bindEvents(): void;
    }
}
