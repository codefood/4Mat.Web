angular.module('app', []);

angular.module('app').controller('mainCtrl',
    function ($scope, $http) {

        $scope.rules = [];
        $scope.documents = [];
        $scope.candidateId = "";
        $scope.employerId = "";
        $scope.file = "";

        $scope.documentTypes = [{ key: "CV", value: 0 },
            { key: "Cover Note", value: 1 },
            { key: "Portfolio", value: 2 },
            { key: "Other", value: 100 }];

        $scope.documentType = $scope.documentTypes[0];

        $scope.loadRules = function () {
            $http({
                method: 'GET',
                url: "/api/Document/ValidationRules"
            })
            .then(function (data) {
                if (data && data.data) {
                    $scope.rules = data.data;
                }
            });
        };

        $scope.upload = function () {
            $http({
                method: 'POST',
                url: '/api/Document',
                data: {
                    candidateId: $scope.candidateId,
                    employerId: $scope.employerId,
                    fileName: $scope.file.name,
                    documentType: $scope.documentType.value
                }
            }).then(function () { alert("Uploaded"); });

            //headers: {
            //    'Content-Type': 'multipart/form-data'
            //},
        };

        $scope.view = function () {
            $http({
                method: 'GET',
                url: "/api/Document/" + $scope.candidateId
            })
            .then(function (data) {
                if (data && data.data) {
                    $scope.documents = data.data;
                }
            });
        };

    });



angular.module('app').directive('file', function () {
    return {
        scope: {
            file: '='
        },
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var file = event.target.files[0];
                scope.file = file ? file : undefined;
                scope.$apply();
            });
        }
    };
});