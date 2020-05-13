
var GigsDetailController = function (followingService) {

    var followButton;

    var init = function () {
        $('.js-toggle-follow').click(toggleFollowings)
    }

    var toggleFollowings = function (e) {
        followButton = $(e.target);

        var followeeId = followButton.attr('data-user-id');

        if (followButton.hasClass("btn-default"))
            followingService.followArtist(followeeId, done, fail);
        else
            followingService.unfollowArtist(followeeId, done, fail)
    }



    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var fail = function () {
        alert('Something failed!')
    }

    return {
        init: init
    }

}(FollowingService);


