When(/^I touch the (\d+)(?:st|nd|rd|th)? view marked "(.*?)"$/) do |ordinal, mark|
  quote = get_selector_quote(mark)
  ordinal = ordinal.to_i - 1

  touch("view marked:#{quote}#{mark}#{quote} index:#{ordinal}")
end