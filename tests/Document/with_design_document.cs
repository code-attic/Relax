using Machine.Specifications;
using Relax.Impl.Model;

namespace Relax.Tests.Document
{
    public abstract class with_design_document
    {
        public static DesignDocument doc;

        private Establish context = () =>
                                        {
                                            doc = new DesignDocument()
                                                      {
                                                          DocumentId = @"design/test",
                                                          Views =
                                                              {
                                                                  {"one", new DesignView() { Map = @"function(doc) { emit(doc); }"}}
                                                              }
                                                      };
                                        };
    }
}