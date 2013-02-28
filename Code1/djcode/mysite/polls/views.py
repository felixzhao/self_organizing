# Create your views here.

from django.template import Context, loader
from django.template import RequestContext
from django.shortcuts import render_to_response, get_object_or_404
from polls.models import Choice, Poll
from django.http import HttpResponseRedirect, HttpResponse
from django.http import Http404
from django.core.urlresolvers import reverse

# start for session

def post_comment(request):
  if request.method != 'POST':
    raise Http404('Only POSTs are allowed')

  if 'comment' not in request.POST:
    raise Http404('Comment not submitted')

  if request.session.get('has_commented', False):
    return HttpResponse("You've already commented.")

  c = comments.Comment(comment=request.POST['comment'])
  c.save()
  request.session['has_commented'] = True
  return HttpResponse('Thanks for your comment!')

def get_session(request):
  if "fav_color" in request.session:
    return HttpResponse("Your favorite color in session is %s" % request.session["fav_color"])
  else:
    return HttpResponse("You don't have a favorite color.")

def set_session(request):
  fav_color_string = 'red'

  response = HttpResponse("Your favorite color in session is %s" % fav_color_string)

#  request.session["fav_color"] = fav_color_string
  request.session['has_commented'] = False

  return response

# end for session

# start for cookie

def show_color(request):
  if "favorite_color" in request.COOKIES:
    return HttpResponse("Your favorite color is %s" % request.COOKIES["favorite_color"])
  else:
    return HttpResponse("You don't have a favorite color.")

def set_color(request):
  if "favorite_color" in request.GET:

    # Create an HttpResponse object...
    response = HttpResponse("Your favorite color is now %s" % request.GET["favorite_color"])

    # ... and set a cookie on the response
    response.set_cookie("favorite_color", 
                        request.GET["favorite_color"])

    return response

  else:
    return HttpResponse("You didn't give a favorite color.")

# end for cookie

def index(request):
  latest_poll_list = Poll.objects.all().order_by('-pub_date')[:5]
  return render_to_response('polls/index.html', {'latest_poll_list': latest_poll_list})
#  t = loader.get_template('polls/index.html')
#  c = Context({
#      'latest_poll_list': latest_poll_list,
#  })
#  return HttpResponse(t.render(c))

#  output = ', '.join([p.question for p in latest_poll_list])
#  return HttpResponse(output)
#  return HttpResponse("Hello, world. You're at the poll index.")

def detail(request, poll_id):
#  try:
#    p = Poll.objects.get(pk = poll_id)
#  except Poll.DoesNotExist:
#    raise Http404
  p = get_object_or_404(Poll, pk=poll_id)
  return render_to_response('polls/detail.html', {'poll': p},
                            context_instance=RequestContext(request))
#  return HttpResponse("You're looking at poll %s." % poll_id)

def results(request, poll_id):
  p = get_object_or_404(Poll, pk=poll_id)
  return render_to_response('polls/results.html', {'poll': p})
#  return HttpResponse("You're looking at the results of poll %s." % poll_id)

def vote(request, poll_id):
  p = get_object_or_404(Poll, pk=poll_id)
  try:
    selected_choice = p.choice_set.get(pk=request.POST['choice'])
  except (KeyError, Choice.DoesNotExist):
    # Redisplay the poll voting form.
    return render_to_response('polls/detail.html', {
        'poll': p,
        'error_message': "You didn't select a choice.",
    }, context_instance=RequestContext(request))
  else:
    selected_choice.votes += 1
    selected_choice.save()
    # Always return an HttpResponseRedirect after successfully dealing
    # with POST data. This prevents data from being posted twice if a
    # user hits the Back button.
    return HttpResponseRedirect(reverse('polls.views.results', args=(p.id,)))
#  return HttpResponse("You're voting on poll %s." % poll_id)
