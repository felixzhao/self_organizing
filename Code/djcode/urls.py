from django.conf.urls import patterns, include, url

urlpatterns = patterns('polls.views',
    url(r'^$', 'index'),
    url(r'^(?P<poll_id>\d+)/$', 'detail'),
    url(r'^(?P<poll_id>\d+)/results/$', 'results'),
    url(r'^(?P<poll_id>\d+)/vote/$', 'vote'),
# cookie
    url(r'^show_color$','show_color'),
    url(r'^set_color$','set_color'),
# session
    url(r'^set_session$','set_session'),
    url(r'^get_session$','get_session'),
    url(r'^post_comment','post_comment'),
)
