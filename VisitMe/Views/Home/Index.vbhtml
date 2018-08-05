@Code
    ViewData("Title") = "Home Page"
End Code

<script>
    $(function () {
        navigator.geolocation.getCurrentPosition(showPosition, positionError);

        function showPosition(position) {
            var coordinates = position.coords;
            $("@ViewBag.lat").val(coordinates.latitude);
            $("@ViewBag.lon").val(coordinates.longitude);
        }

        function positionError(position) {
            alert("Error! Code: " + position.code)
        }
    });
</script>

@Code

End Code