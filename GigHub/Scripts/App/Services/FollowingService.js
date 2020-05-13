var FollowingService = function () {

    var followArtist = function (followeeId, done, fail) {
        $.post('/api/followings', { followeeId: followeeId })
            .done(done)
            .fail(fail)
    }

    var unfollowArtist = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        }).done(done).fail(fail)
    }

    return {
        followArtist: followArtist,
        unfollowArtist: unfollowArtist
    }


}();






